# 02 - Repository (+ Unit of Work)

## Problem / Smell

In [`bad-approach`](bad-approach/OrderService.cs), `OrderService` owns a raw,
`public static List<Order>` and manipulates it with hand-rolled loops
directly inside its methods. [`OrderReportService`](bad-approach/OrderReportService.cs)
needs order data too, but since there's no shared abstraction, it reaches
straight into `OrderService.Orders` and writes its **own**, independently
duplicated filtering logic against it.

This causes three concrete problems:

- **Coupling to a specific storage shape.** If `OrderService.Orders` changes
  representation (a different collection type, or a real database), every
  class that directly touches it breaks or needs rewriting.
- **Duplicated, divergent logic.** `OrderService` and `OrderReportService`
  each wrote their own loops to find/filter orders. A bug fixed in one
  doesn't automatically get fixed in the other - they can silently drift
  apart.
- **Broken encapsulation.** `Orders` being `public static` and mutable means
  *anything* can add/remove/corrupt orders directly (e.g. bypassing
  `PlaceOrder`'s id-assignment logic entirely), not just read them.

## Pattern

**Repository**: an abstraction (`IOrderRepository`) that represents "the
collection of Orders," hiding *how* they're actually stored. Business logic
depends only on this interface - never on a database, a list, a file, or any
other storage detail.

Key design decisions we made along the way (see the commit history):

1. **Keep the interface minimal and storage-shaped.** `IOrderRepository`
   only has `Add`, `Update`, `GetOrderById`, `GetOrdersByCustomer`,
   `GetOrders` - no business filtering (like "active" vs "cancelled") baked
   in. That's a business rule, not a storage concern, so it stays in the
   calling code (`OrderReportService`) as a LINQ `.Where(...)` over whatever
   the repository returns.
2. **`GetOrderById` returns `Order?` (nullable), not an exception.** "Not
   found" is a normal, expected outcome for a query - the repository just
   reports it honestly. The *business logic* (`OrderService.CancelOrder`)
   decides what "not found" means (in our case, throw).
3. **`Update` exists even though the in-memory implementation barely needs
   it.** `Order` is a reference type, so mutating it in place already
   changes the stored object. But `OrderService` calls `Update` anyway,
   because a future database-backed repository *would* need an explicit
   call to persist the change - and `OrderService`'s code shouldn't need to
   change when that swap happens.
4. **Never leak the internal collection.** `GetOrders()`/`GetOrdersByCustomer`
   return a **copy**, not the live internal list - otherwise callers could
   mutate the repository's real storage directly, the exact same
   encapsulation problem we started with.

**Unit of Work**, briefly: when multiple repositories need to commit
changes together as one atomic transaction (e.g. "cancel order" and
"restock inventory" must both succeed or both fail), a single Unit of Work
object coordinates the commit across all of them - rather than each
repository saving independently, risking a partial, inconsistent write. We
didn't need to build one here (a single repository, a single aggregate has
nothing to coordinate), but it's worth knowing: in EF Core, `DbContext`
*already behaves like* a Unit of Work - one `SaveChanges()` call commits
every tracked change at once, which is why a separate `IUnitOfWork`
abstraction is often unnecessary in EF Core codebases specifically, and
matters most when there's no built-in transaction coordinator (e.g. raw
ADO.NET, multiple heterogeneous data sources).

## When to use it

- Whenever business logic needs to read/write persisted data (orders,
  products, customers, ...) and you want that logic testable without a real
  database.
- When more than one part of the codebase needs the same data access - a
  repository gives them one shared, correct place to get it, instead of
  each writing their own queries.
- When you expect the storage technology might change (in-memory → SQL →
  a different ORM) and don't want that change to ripple into business logic.

## When NOT to use it

- For a single, throwaway script or prototype with no real persistence
  need - the abstraction is pure overhead if nothing will ever be swapped
  or tested in isolation.
- When you're already using an ORM like EF Core directly in a thin
  application layer and don't have multiple consumers or a real need to
  swap storage - EF Core's `DbSet<T>` already *is* a reasonably good
  repository/unit-of-work combination; wrapping it in another repository
  layer ("repository over a repository") can be pure ceremony. Introduce
  the extra abstraction only when you have a concrete reason (multiple
  storage technologies, heavy unit-testing needs, or complex queries you
  want centralized).
- Don't let query methods multiply per business filter combination (e.g. a
  method for every combination of "by customer," "by status," "by date").
  That's a sign you need either LINQ filtering in the calling code (what we
  did here) or the **Specification** pattern (#14 in our curriculum) for
  reusable, composable query logic.

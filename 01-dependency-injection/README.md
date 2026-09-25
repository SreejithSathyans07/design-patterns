# 01 - Dependency Injection

## Problem / Smell

In [`bad-approach`](bad-approach/OrderService.cs), `OrderService` builds its own dependencies:

```csharp
private readonly ConsoleLogger _logger = new();
private readonly EmailNotifier _notifier = new();
```

`OrderService` doesn't just *use* a logger and a notifier — it *decides* which
concrete classes they are, at the point they're declared. This causes two
concrete problems:

- **Rigidity.** Adding a new notification channel (e.g. SMS for VIP
  customers) means editing `OrderService.cs` itself, even though nothing
  about order-processing logic actually changed. This violates the
  Open/Closed Principle - a class that already works has to be reopened
  every time an unrelated implementation detail changes.
- **Untestability.** There's no seam where a test can substitute a fake
  logger or notifier. The only way to verify `ProcessOrder` worked is to
  capture real `Console.WriteLine` output - fragile, and it can't cleanly
  distinguish "was the email sent?" from "was this line logged?".

## Pattern

**Dependency Injection**: a class declares what it needs (as interfaces/
abstractions) via its constructor, instead of creating those dependencies
itself. Something *outside* the class - the **composition root** (here,
`Program.cs`) - decides which concrete implementations to hand it.

We got there in four steps (see the commit history of `better-approach`):

1. **Extract interfaces** (`IAppLogger`, `INotifier`) from the concrete
   classes. This alone doesn't fix anything yet, but it defines the
   contract dependents will use.
2. **Depend on the interface type**, not the concrete class, for the field
   declarations (`IAppLogger _logger = new ConsoleLogger();`). This
   restricts the rest of the class to only calling members the interface
   declares - the concrete type becomes referenced in exactly one place.
3. **Inject via the constructor.** `OrderService` receives an `IAppLogger`
   and `INotifier` as constructor parameters instead of creating them.
   `Program.cs` becomes the composition root - the one place allowed to
   know about `ConsoleLogger`/`EmailNotifier`.
4. **Test with hand-written fakes.** Because `OrderService` only depends on
   interfaces, a test can inject `FakeLogger`/`FakeNotifier` (plain classes
   that record calls instead of doing real I/O) and assert on real
   `OrderService` behavior with zero side effects. See
   [`better-approach.Tests`](../01-dependency-injection/better-approach.Tests).

## When to use it

- Any class that talks to something with a real-world side effect (network,
  disk, email/SMS providers, databases, the system clock) - so tests can
  substitute a fake.
- Whenever you expect to swap an implementation later (different payment
  provider, different notification channel, different data store) without
  touching the class that uses it.
- As the default way to wire up services in a codebase of meaningful size -
  it's what makes the other patterns in this repo (Strategy, Factory,
  Decorator, ...) composable in practice.

## When NOT to use it

- For simple, pure, stateless logic with no external side effects and no
  realistic reason to swap implementations (e.g. a math/formatting helper)
  - injecting an interface there is ceremony without payoff.
  - a `static` method is often more honest.
- Don't inject *everything* reflexively. If a dependency is a stable,
  well-known concrete type with no variation and no testing need (e.g. a
  simple value object), a constructor parameter of the concrete type is
  fine - DI is a tool for managing *variation and side effects*, not a
  rule to apply universally.
- Watch out for "constructor over-injection" - a constructor that takes 8+
  dependencies is usually a sign the class has too many responsibilities,
  not a sign you need a DI container.

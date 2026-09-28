# Patterns Summary

A running comparison of every pattern covered so far: what was wrong in
`bad-approach`, how the pattern fixed it, and the core signal that tells you
to reach for it. Append a row each time a pattern is completed - see
`PROGRESS.md` for step-by-step status and `CLAUDE.md` for the full plan.

| # | Pattern | The Problem (in `bad-approach`) | How the Pattern Solved It | The Core Trigger to Reach for It |
|---|---|---|---|---|
| 01 | **Dependency Injection** | `OrderService` created its own concrete dependencies (`new ConsoleLogger()`, `new EmailNotifier()`) directly inside itself - untestable without real console/email side effects, and rigid (swapping a channel meant editing `OrderService`'s own code). | Extracted interfaces (`IAppLogger`, `INotifier`); `OrderService` depends only on the interface and receives concrete implementations via its constructor. `Program.cs` became the single composition root deciding concrete types. | A class needs a dependency with a real-world side effect (I/O, external system), and there's **exactly one** implementation active for the whole run of the app, decided once at startup. |
| 02 | **Repository (+ Unit of Work)** | `OrderService` owned a raw `public static List<Order>` manipulated with inline hand-rolled loops. `OrderReportService` needed the same data but had no shared abstraction, so it wrote its **own** independent (buggy) query logic against the same list - plus the public mutable field broke encapsulation entirely. | Extracted `IOrderRepository` - a small, storage-shaped interface (`Add`/`Update`/`GetOrderById`/`GetOrdersByCustomer`/`GetOrders`), deliberately *without* business filtering baked in. Both consumers receive it via constructor injection (reusing DI). Business rules (e.g. "active" = not cancelled) stayed in the calling code. | **Multiple parts of the app need the same persisted collection**, and you want *how/where it's stored* hidden behind one shared abstraction instead of duplicated queries scattered everywhere. |
| 03 | **Strategy** | `NotificationService` picked a channel via an if/else chain embedded in its method. `ReminderService` needed the identical decision, wrote its own independent if/else, and it silently drifted out of sync (forgot the push-notification branch) - a real, observable bug. | Extracted `INotificationStrategy` (one method: `Send`); each channel (SMS/Push/Email) became its own class implementing it. Centralized the "which one" decision in a single `NotificationStrategyFactory.GetStrategy(order)`, injected into every consumer. | **Multiple interchangeable implementations of the same job coexist at once**, and *which one applies is decided fresh, per call, based on data* - not fixed once at startup. |

## The DI vs. Strategy distinction (the one that trips people up)

DI is the plumbing: a class receives a dependency via its constructor
instead of creating it itself. Repository and Strategy are both specific
*situations* where you need that plumbing - they don't replace DI, they use
it:

- **Repository** needs it because multiple classes share one persisted
  collection, and storage details should be hidden behind one abstraction.
- **Strategy** needs it because the running app must choose between several
  *live* implementations, differently, on every call, based on data - not
  fixed once at startup (that's the difference from plain DI, where there's
  normally exactly one implementation for the app's whole lifetime).

The practical test: *"Will this dependency ever need to be a different
concrete thing on the next call, based on data I only have in hand right
then?"* No → you just needed DI. Yes → that's Strategy, and you'll need a
factory (or similar) alongside it to make that per-call decision.

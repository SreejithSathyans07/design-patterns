# Progress

Tracks where we are in the design patterns curriculum (see `CLAUDE.md` for
the full plan and conventions). Checkboxes are ticked by hand, not
automatically - a step being *coded* isn't the same as it being *understood*.
Tick a box only when you're confident you could explain that step to someone
else without looking it up.

## 01 - Dependency Injection

- [ ] `bad-approach` scaffolded (OrderService directly `new`s up
      ConsoleLogger + EmailNotifier)
- [ ] `better-approach` started as an exact copy of `bad-approach`
- [ ] Step 1: extracted `IAppLogger` / `INotifier` interfaces
- [ ] Step 2: fields declared as the interface types, not concrete classes
- [ ] Step 3: dependencies injected via `OrderService` constructor;
      `Program.cs` is the composition root
- [ ] `better-approach.Tests` project created, referencing `better-approach`
- [ ] Hand-written `FakeLogger` / `FakeNotifier` test doubles written
- [ ] Unit tests written and passing (`OrderServiceTests`)
- [ ] `01-dependency-injection/README.md` filled in (Problem / Pattern /
      When to use / When not to use)
- [ ] **Pattern complete** - comfortable explaining DI and the
      composition-root concept unprompted

## 02 - Repository (+ Unit of Work)

- [ ] `bad-approach` scaffolded (OrderService owns a raw static `List<Order>`
      with inline CRUD logic; OrderReportService duplicates its own
      hand-rolled query logic directly against that same list)
- [ ] `better-approach` started as an exact copy of `bad-approach`
- [ ] Step 1: extracted `IOrderRepository` interface (the operations both
      OrderService and OrderReportService actually need)
- [ ] Step 2: moved storage + query logic into `InMemoryOrderRepository`
- [ ] Step 3: injected `IOrderRepository` into `OrderService` and
      `OrderReportService` via constructor (reusing DI from pattern 01)
- [ ] Discussed Unit of Work - when multiple repository changes need to
      commit together, and why we do/don't need it for this example
- [ ] `better-approach.Tests` project created, referencing `better-approach`
- [ ] Unit tests written using a fake `IOrderRepository`, without any real
      storage or duplicated query logic
- [ ] `02-repository-pattern/README.md` filled in
- [ ] **Pattern complete** - comfortable explaining why Repository removes
      duplication and enables swapping storage without touching business
      logic

## 03 - Strategy

- [ ] `bad-approach` scaffolded
- [ ] `better-approach` copied and refactored
- [ ] Tests added
- [ ] README filled in
- [ ] **Pattern complete**

## 04 - Factory (Simple / Factory Method / Abstract Factory)

- [ ] `bad-approach` scaffolded
- [ ] `better-approach` copied and refactored
- [ ] Tests added
- [ ] README filled in
- [ ] **Pattern complete**

## 05 - Decorator

- [ ] `bad-approach` scaffolded
- [ ] `better-approach` copied and refactored
- [ ] Tests added
- [ ] README filled in
- [ ] **Pattern complete**

## 06 - Builder

- [ ] `bad-approach` scaffolded
- [ ] `better-approach` copied and refactored
- [ ] Tests added
- [ ] README filled in
- [ ] **Pattern complete**

## 07 - Adapter

- [ ] `bad-approach` scaffolded
- [ ] `better-approach` copied and refactored
- [ ] Tests added
- [ ] README filled in
- [ ] **Pattern complete**

## 08 - Observer

- [ ] Not started

## 09 - Mediator / CQRS

- [ ] Not started

## 10 - Chain of Responsibility

- [ ] Not started

## 11 - Singleton

- [ ] Not started

## 12 - Facade

- [ ] Not started

## 13 - Template Method

- [ ] Not started

## 14 - Specification

- [ ] Not started

## 15+ - Tier 3 (pick as needed)

Command, State, Proxy, Composite, Result pattern, Outbox pattern.

- [ ] Not started

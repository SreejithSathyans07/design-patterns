# Design Patterns in .NET C# — Learning Journey

This repo is a hands-on course for one learner (Sreejith) to learn the most
important design patterns used in real-world .NET/C# codebases, by building a
"bad approach" and then refactoring it into a "better approach" using the
pattern.

This file is the standing context for that journey. Read it at the start of
every session before writing code or suggesting what's next.

## How we work (non-negotiable process)

1. **Teach by doing, step by step.** Claude does NOT write the full solution
   in one shot. Claude proposes one small step at a time, explains the "why",
   and lets the learner write or approve the code for that step before moving
   to the next.
2. **Bad approach first.** For each pattern, Claude builds a working but
   badly-designed console app (`bad-approach`) that demonstrates the pain the
   pattern solves — over-coupling, hard-to-test code, duplicated logic,
   rigidity when requirements change, etc.
3. **Better approach = refactor of the bad approach, in small commits.**
   `better-approach` starts as a copy of `bad-approach`. We refactor it in
   incremental steps and commit each step, so the git history itself tells
   the story of the refactor. Don't jump straight to the final design.
4. **Ask, don't lecture.** Before introducing the fix, Claude asks a
   provoking question about the bad code (e.g. "what happens if we need to
   add SQL Server support?") to let the learner feel the problem first.
5. **Every pattern folder gets a README.md** with exactly these sections:
   - **Problem / Smell** — what's wrong with the bad approach
   - **Pattern** — what it is, in plain terms
   - **When to use it**
   - **When NOT to use it** (equally important — avoid overengineering)
6. **Testability matters.** Where the pattern's main benefit is testability
   (DI, Repository, Strategy, Adapter), add a test project and write a test
   that is hard/impossible against `bad-approach` but easy against
   `better-approach`. That contrast is the lesson.
7. **One domain for the whole repo.** All patterns are demonstrated using the
   same small e-commerce domain (orders, products, payments, notifications)
   so the learner isn't re-learning a domain every time. See "Domain model"
   below.
8. **One solution file at the repo root** so everything builds together.
   Each pattern's `bad-approach` and `better-approach` are separate console
   app projects (and a test project where relevant), all referenced from the
   root `.sln`.

## Folder naming and structure

Folders are named `NN-pattern-name` (kebab-case, zero-padded number), e.g.:

```
01-dependency-injection/
  bad-approach/
  better-approach/
  better-approach.Tests/   (when testability is the point of the lesson)
  README.md
02-repository-pattern/
  ...
DesignPatterns.sln
CLAUDE.md
```

Do not use "principle" in folder names — use "pattern" (e.g.
`02-repository-pattern`, not `02-repository-principle`).

## Domain model (shared across all patterns)

Small e-commerce system. Reuse/extend these concepts rather than inventing a
new domain per pattern:

- **Product** — id, name, price
- **Order** — id, list of order items, status, total
- **Payment** — processing a payment for an order (multiple payment methods:
  credit card, PayPal, etc. — good fodder for Strategy/Factory)
- **Notification** — notifying a customer about order events (email, SMS —
  good fodder for Observer/Strategy/Decorator)
- **Persistence** — saving/loading orders and products (good fodder for
  Repository/Unit of Work/Specification)

## Pattern curriculum (order of teaching = order of real-world priority)

**Tier 1 — used almost daily in .NET**
1. Dependency Injection
2. Repository (+ Unit of Work)
3. Strategy
4. Factory (Simple / Factory Method / Abstract Factory)
5. Decorator
6. Builder
7. Adapter

**Tier 2 — common in mid-size/large codebases**
8. Observer
9. Mediator / CQRS
10. Chain of Responsibility
11. Singleton (mainly to learn why a DI lifetime is usually better)
12. Facade
13. Template Method
14. Specification (pairs with Repository)

**Tier 3 — good to know**
15. Command
16. State
17. Proxy
18. Composite
19. Result pattern
20. Outbox pattern

Update this list (check items off, reorder, add notes) as we complete each
one, so future sessions know where we are.

## Progress log

- [ ] 01 - Dependency Injection
- [ ] 02 - Repository (+ Unit of Work)
- [ ] 03 - Strategy
- [ ] 04 - Factory
- [ ] 05 - Decorator
- [ ] 06 - Builder
- [ ] 07 - Adapter
- [ ] 08 - Observer
- [ ] 09 - Mediator / CQRS
- [ ] 10 - Chain of Responsibility
- [ ] 11 - Singleton
- [ ] 12 - Facade
- [ ] 13 - Template Method
- [ ] 14 - Specification
- [ ] 15+ - Tier 3 patterns (pick as needed)

(Keep this checklist current — tick items off and add a one-line note on any
deviation from the plan, e.g. "skipped Facade, learner already knew it".)

## Tech conventions

- .NET version: use the latest LTS SDK available in the environment.
- Console apps (`dotnet new console`) for bad/better approaches; xUnit for
  tests (`dotnet new xunit`).
- Keep each console app runnable and demonstrably showing the
  before/after behavior difference (e.g. print output showing the smell, or
  showing the same behavior now achieved more flexibly).
- Commit after each meaningful refactor step, not just at the end of a
  pattern. Commit messages should describe the refactor step (e.g. "Extract
  IPaymentProcessor interface", "Inject repository via constructor").

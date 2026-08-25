# Senior Developer Technical Assessment

Station Fault Logger - a slimmed-down slice of the Interaction Platform (IXP).

Time box: 45 minutes.

You are not rewriting the app. Run it, find defects, and fix the highest-severity ones in code. Talk through what you are doing.

---

## What you will do

1. Start the API and the web app (commands below).
2. Use the product: list faults, log a new fault, go back to the list.
3. Find defects (functional, auth, cache, contract, tests).
4. Fix as many P1 issues as you can. Quality of diagnosis beats a long laundry list.

You may use docs, the IDE, DevTools, and the test runners. Some tests pass for the wrong reason.

---

## Stack (mirrors IXP)

| Layer | Tech |
| --- | --- |
| Web | Next.js App Router, React 19, TypeScript |
| Data / forms | TanStack Query, Axios, react-hook-form, Zod |
| Client state | Zustand (offline draft queue) |
| API | ASP.NET Core (.NET 8 LTS; IXP production is .NET 10), FluentValidation, in-memory cache (stand-in for Redis) |
| Cross-cutting | Correlation IDs, role-based access (`Forms.FaultsReader`) |
| Tests | Jest + Testing Library, xUnit |


---

## Setup

Requires Node 22+ and the .NET 8 SDK. On managed Windows devices, Defender ASR may block freshly built `.exe` files - this project sets `UseAppHost=false` so `dotnet run` launches via the SDK host instead.

```bash
# Terminal 1 - API (http://localhost:5080/swagger)
cd api
dotnet run --project src/Ixp.Interview.Api --no-launch-profile --urls http://localhost:5080

# Terminal 2 - Web (http://localhost:3000)
cd web
npm install
npm run dev
```
```bash
# API tests
cd api
dotnet test

# Web tests
cd web
npm test
```

---

## Product brief

Inspectors log station faults during an SQ inspection.

- The list must show submitted faults only.
- You are signed in as Alex Patel, role `Forms.Inspector`. Alex must see only their own faults.
- Users with `Forms.FaultsReader` may see everyone’s submitted faults.
- Creating a fault should persist it and the list should refresh.
- Description is required.

Seeded data includes faults from Alex and from Sam  (`Forms.FaultsReader`).

---

## What we evaluate

- Root-cause debugging (file + why, not just symptoms)
- AuthZ / data-scoping instincts
- React Query cache keys and server cache invalidation
- Contract correctness (Zod + FluentValidation)
- Client/server boundaries (what must never ship to the browser)
- Test quality (spotting false confidence)
- Communication under a time box

Good luck.

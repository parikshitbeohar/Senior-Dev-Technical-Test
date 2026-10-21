# Notes

I ran the app as Alex and as Sam, compared it with the brief, and fixed what I found. One commit per fix, in the order I did them.

## What I fixed

1. List was empty on first load (paging off by one).
2. Prev/Next buttons now disabled at the first and last page.
3. Debounced the filters so typing doesn't call the API on every key.
4. Alex could see everyone's faults (role check was inverted).
5. Drafts were showing in the list.
6. New fault didn't show after saving (server cache and client query key).
7. Description wasn't required (API and form), and the web test was asserting the bug.
8. Any fault could be opened by id (added an ownership check).
9. API key and user identity were visible in the browser (moved behind a server-side proxy).
10. API no longer falls back to Alex when identity headers are missing.
11. Removed a test that could never fail.
12. Offline queue: stopped duplicate sends, refreshes the list after sending, and survives a page refresh.

## Not done

- Identity still comes from headers (production should validate a token).
- API key is in the repo (should come from config or a secret store).
- List cache key doesn't include the role.
- Offline: no message or pending list for the user, no separation of network errors from rejected items, no duplicate protection if a response is lost.


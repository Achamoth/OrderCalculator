# Instructions
- Should simply be able to open and build/run the solution in Visual Studio.
- Ensure that 'order.json' file and 'Data' folder are copied to the bin directory (should be done automatically).

# Notes
## High level
- Tried to make as much configurable as possible (i.e. delivery thresholds and fees, taxes, book list etc.).
- Used json for inbound/outbound requests/responses due to versatility and wide compatability.

## Assumptions
- Was unsure whether the delivery fee threshold is compared against pre-tax or post-tax order total; assumed post-tax.
- Assumed that delivery fee has GST applied to it; tried a cursory search online to discern this and it seems complex.
- Had to assume that book name + author names are a unique combination due to absence of ISBN.

## Trade-offs
- Data storage solution is sub-optimal; json document store probably unsuitable for real store management system, but facilitates rapid development (would use RDBMS).
- Kept discount logic simple, only catering for one discount at a time per genre. Would have to extend to service other use-cases i.e. discounts spanning multiple genres, multiple discounts stacking etc.
- Genres are hardcoded as enums; assumed to be relatively static for the time being.

## Areas I would improve
- Expand test coverage.
- Handle more exceptions and errors throughout the program,  especially with IO.
- Uniquely identify books by ISBN rather than title and author.
- More configurability for taxation and discounts.
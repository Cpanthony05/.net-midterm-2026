# BUGS

The starter contains behaviors that contradict `ASSIGNMENT.md`. Write here every discrepancy you found.
Fill it in yourself, in your own words. There is no required number: what counts is that they are real.

| Which rule from ASSIGNMENT.md is violated (R1–R8) | Where the problem was in the starter (file, approximate line) | How you discovered it (test, reading, AI) | What you changed |
|---|---|---|---|
| R5 | services/bookingservice.cs | code reading, AI | reject bookings with start time before current moment with 400 error |
| R6 | services/bookingservice.cs | code reading, AI | reject bookings with difference between end and start higher than 4 hours
| R7 | services/bookingservice.cs | code reading, AI | using strict comparisons
| R8 | services/bookingservice.cs | code reading, AI | if the status is cancelled, return 409, otherwise do transition from active to cancelled
 

# AI-LOG

Write down, in order, what you asked the AI. It is not a punishment: a missing or false log lowers the level D grade.

| Time (approx.) | What you asked (briefly) | What you kept | What you changed or rejected |
|---|---|---|---|
| 5 minutes | before the test began, i had a markdown that described the course project code arhitecture. I gave that and the assignment.md file to the AI and told it to change the current project to the coding style mentioned in the two markdowns. I told it that ASSIGNMENT.md should always have priority over the other one | the project structure is good  | many classes/enums were in the same file |
| 3 minutes | I gave the AI the requirement to not allow more than 2 active bookins per day | it was good | i kept it as it is
| 1 minute | some classes/enums are in the same file (example BookingStatus enum is in Booking class file). make sure everything lives in its own file | good | nothing
| 1 minute | i asked AI to write a .gitignore | i kept it | i didnt change it
| 3minutes | Two new requirements : "R10. Rooms with a capacity greater than 10 can be booked for at most 2 hours (exactly 2 hours is allowed). A longer booking is rejected with 400. Rooms with capacity 10 or less keep the 4-hour limit from R6. R11. A booking can be cancelled only if it starts at least 60 minutes from now (current time from TimeProvider). Otherwise the cancel request is rejected with 409.". Add them to ASSIGNMENT.md, modify the project to comply to these requirements. | good | didnt change
| 5minutes | I gave the nitial project to claude and told it to look for things that didnt respect the ASSIGNMENTS.md so I can start filling BUGS.md | the output was good | nothing

A simple .NET Core app to allow employers to preview employee benefits and deductions. A fun little exercise in from-scratch development.

This was crunched-out in a couple days after I got back from my trip to NC, and although I think it's readable and functional there are a few pitfalls 
I will address when I get time:

1) I leaned pretty hard on computed properties, and while they are really handy for quick Linq calculations they don't play nice with EF. 
2) There really wasn't much time for test coverage
3) I wanted to implement a handy multi-select many-to-many form for employees and dependents but ran out of time. Not a huge deal for the purposes of 
this demo, as you can add dependents piecemeal.
4) I figured implementing contrived discounts as configurable expression trees would be the most business-friendly and futureproof. You can be 
data-driven, and run them as needed. What I settled on is very flexible but not as elegant, 
which ended up being injecting delegates unto functions defined in the database after retrieval. This allows for a lot of flexibility, but isn't the 
most beautiful code in the world. It has the additional complication of requiring data to be injected into my models
to drive computed properties based on discounts, which further complicates things. If I had a bit more time I'd focus on getting around this as my 
first priority.
5) Really not enough time for extensive error handling/logging


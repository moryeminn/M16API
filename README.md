Good morning, Agent. 
MI6 is trying to gather statistics about its missions.
Your mission, should you choose to accept it, has two parts:

Part 1:
Endpoint: POST /mission
Body: {“agent”: “[codename]”, “country”: “[country]”, “address”: “[address string]”, “date”: “[date and time]”}
Adds a mission.
See the sample data below.

Part 2: 
Endpoint: GET /countries-by-isolation
An isolated agent is defined as an agent that participated in a single mission.
Implement an algorithm that finds the most isolated country (the country with the highest degree of isolated agents).
For the sample input (see below) input:
•	Brazil has 1 isolated agent (008) and 2 non-isolated agents (007, 005)
•	Poland has 2 isolated agents (011, 013) and one non-isolated agent (005)
•	Morocco has 3 isolated agents (002, 009, 003) and one non-isolated agent (007)

So the result is Morocco with an isolation degree of 3.

Part 3:
Find the closest mission from a specific address (any existing mission’s address)

Endpoint: POST /find-closest
          Body: {“target-location”: “[an address or geo coordinates]”}

Note: you can use external API for this (like google)
Your task is to write a service that uses any type of DB (pre-populated with the sample data) and exposes the above endpoints. You can use any framework you see fit.

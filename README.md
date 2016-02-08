# NewsData

Analyse news article's text, using AlchemyAPI/IBM's natural language processing algorithms.

Queries are currently configured to find articles with the word 'Bitcoin' in the title. Once these are retrieved, a second query is made to assess sentiment of the articles text in relation to 'Bitcoin'

I use a worker role published as a webjob scheduled to perodically retrieve the latest articles and their sentiment values and then store them in a SQL database.

To enable analysis of retrieved values, a ASP.NET 4.5 WebAPI2 website is included in the solution. Currently adding Javascript front end to graph values vs time.

Nice to haves would include modeling the IBM queries better.

Full azure solution although can be run on a local machine - just change DB context connection strings (remove for SQLexpress)

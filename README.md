# IKIGAI : Purpose planning


[check it out](https://travelglobe.azurewebsites.net/index.html)

> Please not that it is running on a shared plan. Startup could be slow for cold start.

## Approach

App doesn't support authentication as of now because it was not in the scope. However each user is assigned a name (stored `client side`).

### Backend

- Made using C# and asp core
- Using rest api including ODATA for query
- Data stored in simple `sqlite db`
- Used c# because it is strictly typed language and entity framework
- Docker support to host almost anywhere containers are supported

### Frontend

Frontend build using `vanilla js, vue and bootstrap`

There is a toggle major button to show intermediaries stages

Moving, deleting, editing requies to click

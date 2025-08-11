# Probability Tool

## ProbTool .Net API Project

- I've used .Net 8 to make it easy to enable the Swagger interface.
- It should serve on https://localhost:someport
- On my machine, this is https://localhost:7159
- There are a few tests which should run without any changes
- A serilog log is created in the logs directory under the project

The request should look something like this

```
  {
    "probFun": "Combined",
    "prob1": 0.5,
    "prob2": 0.5
  }
```

or for the Either method

```
  {
    "probFun": "Either",
    "prob1": 0.5,
    "prob2": 0.5
  }
```

## prob-tool React app

- I've created this using React and TypeScript
- There is a .env file in the project root which provides the API URL
- npm test should run the tests without anything else running. There are more tests that I think could/should be created
-

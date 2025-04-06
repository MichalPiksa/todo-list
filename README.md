# TODO List API

## Overview

This project is an API designed to manage tasks in a TODO list. The API provides two primary endpoints: one for creating a new task and another for retrieving tasks.

## Basic Requirements

### Creating a New Task

Develop an endpoint that allows users to create a new task. Each task must include:

- A title (**mandatory**)
- A due date and time (**mandatory**)
- An optional description

**Validation:**

- If a task is created with a due date and time in the past, return an error (in accordance with REST principles).
- If a task is created without a title, return an error (in accordance with REST principles).

### Retrieving Tasks

Develop an endpoint to fetch tasks. By default (when no parameters are provided), the endpoint should return all tasks with a future due date.

Additionally, ensure that the endpoint can retrieve:

- The task closest to its due date.
- All tasks with a past due date.
- A complete list of all tasks stored in the application.


## Additional Features (Optional Enhancements)

For those who complete the basic requirements and want to extend the functionality, consider implementing the following:

### Extended Task Retrieval

Enhance the task retrieval endpoint to support fetching:

- All tasks that are due today.
- All tasks due within the current week.

### Task Status Management

Implement functionality to categorize tasks into the following statuses:

- New (not started yet)
- Active (currently in progress)
- Done (completed)

Modify the GET endpoint to allow filtering tasks based on their status (new, active, done).

### Task Deletion

Enable task deletion with the following restriction:

- A task can only be deleted if it is in the new status.
- If a user attempts to delete a task in any other status, return an error (in accordance with REST principles).

### User Interface

Create a simple UI to manage tasks, including:

- A form for creating new tasks.
- A view displaying all tasks.
- Functionality to delete tasks and change their status to active or done.

This API is structured to follow REST principles and ensures efficient task management with clear validation rules and additional features for e
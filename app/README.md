# MrWorldWide - Frontend

A React-based web application to keep tabs on a digital nomad's adventures, showcasing their current location, blog posts, and photo journals.

## Getting Started

To set up a local development environment, make sure you have the following dependencies installed:

- [Node.js](https://nodejs.org/) (LTS version recommended)
- [npm](https://www.npmjs.com/) (bundled with Node.js)

Once the dependencies are installed, follow these steps:

1. Clone the repository and navigate to the `app` directory.
2. Run `npm install` to install the required packages.
3. Run `npm start` to start the development server.

## Technical Details

1. **User Interface**: The frontend is built using [React](https://reactjs.org/), a popular and performant JavaScript library for building user interfaces. React enables the creation of reusable UI components, making it easy to develop and maintain complex applications.

2. **Styling and Theming**: The application uses [@mui/material](https://mui.com/) for its UI components and theming. MUI is a comprehensive and customizable library of React components that follow the Material Design guidelines. The app also leverages [@emotion/react](https://emotion.sh/docs/@emotion/react) and [@emotion/styled](https://emotion.sh/docs/@emotion/styled) for additional styling and theming capabilities.

3. **Date and Time Handling**: The app uses [Moment.js](https://momentjs.com/) and [moment-timezone](https://momentjs.com/timezone/) for date and time manipulation and formatting. In conjunction with the @mui/x-date-pickers, @mui/lab, and @date-io/moment packages, this ensures a consistent and user-friendly date and time experience throughout the application.

4. **API Requests**: [Axios](https://axios-http.com/) is employed for making HTTP requests to the backend API. This library simplifies API interactions by providing an easy-to-use interface for making requests and handling responses.

5. **Form Handling**: The app uses [Formik](https://formik.org/) for form handling, along with [Yup](https://github.com/jquense/yup) for form validation. Formik simplifies the process of building, validating, and handling form submissions, while Yup provides a powerful schema-based approach to validation.

6. **Routing**: [react-router-dom](https://reactrouter.com/web/guides/quick-start) is used to handle client-side routing within the application. This library allows for the creation of dynamic and responsive routing, enabling seamless navigation through the various pages and components of the app.

7. **Authentication**: [oidc-client-ts](https://github.com/leastprivilege/oidc-client-ts) is employed for handling OpenID Connect authentication. This library enables secure user authentication and authorization, ensuring that only authorized users can access certain features of the application.

8. **TypeScript**: The app is built with [TypeScript](https://www.typescriptlang.org/), a statically typed superset of JavaScript. TypeScript offers improved code quality and maintainability by enabling type-checking, autocompletion, and other useful features.

9. **Development Environment**: The frontend uses [react-scripts](https://www.npmjs.com/package/react-scripts) to simplify the development workflow, including starting the development server, building the application for production, and running tests. Environment-specific build scripts are available for development (`build:dev`) and production (`build:prod`), which use [env-cmd](https://www.npmjs.com/package/env-cmd) to load the appropriate environment variables.

10. **Linting**: The codebase is linted with [ESLint](https://eslint.org/), which enforces consistent code style and catches potential errors early in the development process. The project uses the [eslint-plugin-react](https://www.npmjs.com/package/eslint-plugin-react) and [@typescript-eslint/parser](https://www.npmjs.com/package/@typescript-eslint/parser) plugins to provide specific rules and configurations for React and TypeScript.

## Contributing

When contributing to the project, please ensure that your code adheres to the established coding standards and best practices. Be sure to write clear, concise, and well-documented code, and include appropriate tests for any new features or bug fixes.


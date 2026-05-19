# Copilot Instructions

## Commit Messages

- Use [conventional commits](https://www.conventionalcommits.org/en/v1.0.0/) message format. Format: `<type>(<scope>): <description>`
- Include scope in the title.
- The subject should be in present tense describing the overall changes as a completed action.
- The next paragraph should be an overall description of the changes.
- Subsequent lines should be a bulleted list of the changes.

Example 1:

```
feat(services): add new user registration endpoint

Added a new API endpoint for user registration that accepts email and password, creates a new user in the database, and returns a JWT token.

- Created `RegisterUserCommand` and corresponding handler in `GreenField.Services`
- Added `POST /api/register` endpoint in `API/Controllers/AuthController.cs`
- Updated database schema with new `Users` table via Inflatable mapping
```

Example 2:

```
fix(frontend): resolve issue with theme variable loading

Fixed a bug where theme CSS variables were not loading correctly on initial page load, causing the default theme to be applied instead of the user's selected theme.

- Updated `ThemeService` to ensure CSS variables are applied before the first render
- Added unit tests for `ThemeService` to verify correct variable application
```

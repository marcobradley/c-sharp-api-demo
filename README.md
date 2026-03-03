# c-sharp-api-demo

## NuGet source configuration

This workspace includes [NuGet.Config](NuGet.Config) with a cleared source list and a single `nuget.org` feed:

- `https://api.nuget.org/v3/index.json`

This avoids package resolution issues in the C# language server and ensures consistent restore behavior across environments.

## Release commits (semantic-release)

GitHub releases are generated from commit messages using Conventional Commits.

The release config in [package.json](package.json) uses plugin option format with an explicit preset:

Required package for this preset: `conventional-changelog-conventionalcommits`.

- `@semantic-release/commit-analyzer` with `{ "preset": "conventionalcommits" }`
- `@semantic-release/release-notes-generator` with `{ "preset": "conventionalcommits" }`
- `@semantic-release/github`

- `feat: ...` → minor version bump
- `fix: ...` → patch version bump
- `feat!: ...` or a commit body containing `BREAKING CHANGE:` → major version bump

Examples:

- `feat(api): add weather forecast endpoint`
- `fix(ci): correct release workflow token usage`
- `feat!: rename health check route`
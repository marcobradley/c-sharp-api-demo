# csharp-api-demo

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

## Windows POST caveat (Kubernetes ingress)

When testing through host-based ingress (for example `csharp-api.localhost`), command behavior differs between `curl.exe` and PowerShell cmdlets on Windows.

- `curl.exe` can call `http://csharp-api.localhost:8080`, but use `--%` so JSON is passed unchanged.
- `Invoke-RestMethod` may fail to resolve `csharp-api.localhost`; use `http://localhost:8080` and send the ingress host in the `Host` header.

```powershell
curl.exe --% -i http://csharp-api.localhost:8080/besttimetobyorsellstock -H "Content-Type: application/json" -d "{\"prices\":[7,1,5,3,6,4]}"
```

```powershell
Invoke-RestMethod -Method Post -Uri "http://localhost:8080/besttimetobyorsellstock" -Headers @{ Host = "csharp-api.localhost" } -ContentType "application/json" -Body '{"prices":[7,1,5,3,6,4]}'
```

Expected response body for both commands: `5`

## Troubleshooting POST responses

- `HTTP 400` with empty body usually means request model binding failed.
- For `/besttimetobyorsellstock`, send an object payload: `{"prices":[7,1,5,3,6,4]}` (not a raw array).
- For PowerShell cmdlets, if `csharp-api.localhost` cannot be resolved, call `http://localhost:8080` and set `-Headers @{ Host = "csharp-api.localhost" }`.
- For `curl.exe` in PowerShell, use `--%` to avoid argument/escaping issues.
- Add `-i` (curl) or inspect `$Error[0]` / `-Verbose` (PowerShell) to quickly see status and transport errors.
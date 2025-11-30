# Issue Tracking & Project Management

**GitHub Issues workflow and team communication.**

---

## Issue Tracking

**Tool:** GitHub Issues  
**Repository:** https://github.com/sullinsje/CSCI-4250-Student-Tracker-App/issues

### Creating an Issue

1. Navigate to Issues → New issue
2. Select template (Bug, Feature, Documentation)
3. Fill all fields
4. Add labels and assign milestone
5. Submit

### Issue Workflow

```
OPEN → TRIAGED → IN PROGRESS → PR SUBMITTED → CLOSED
```

---

## Labels

### Type
| Label | Usage |
|-------|-------|
| `bug` | Unintended behavior |
| `enhancement` | New feature |
| `documentation` | Docs updates |
| `refactor` | Code reorganization |

### Priority
| Label | SLA |
|-------|-----|
| `priority-critical` | Fix within 24 hours |
| `priority-high` | Fix within 48 hours |
| `priority-medium` | Fix within 1 week |
| `priority-low` | Fix when time allows |

---

## Issue Templates

### Bug Report
```
## Description
What's wrong?

## Steps to Reproduce
1. Step 1
2. Step 2

## Expected Behavior
What should happen

## Actual Behavior
What actually happens

## Environment
- OS: Windows 10 / macOS / Linux
- .NET Version: 9.0
- Browser: Chrome / Firefox
```

### Feature Request
```
## Description
What feature is needed?

## Problem It Solves
What problem does this address?

## Acceptance Criteria
- [ ] Criterion 1
- [ ] Criterion 2
```

---

## Linking Issues to Work

**In Commit Messages:**
```
feat(attendance): add CSV export

Closes #42
```

**In Pull Requests:**
```
## Related Issues
Closes #42
Closes #51
```

**Keywords:** `Closes`, `Fixes`, `Resolves`, `Relates to`

---

## Communication

**Synchronous:**
- Daily standup
- GitHub PR comments
- Slack/Teams for urgent issues

**Asynchronous:**
- GitHub Issues for feature discussions
- PR descriptions for context
- Commit messages for history

**Best Practices:**
- ✅ Be specific with examples and context
- ✅ Use code blocks for error messages
- ✅ Link related issues
- ✅ Ask questions clearly
- ❌ Avoid all CAPS or dismissive tone

---

## Milestones

Create milestones for releases:
```
v1.0.0 - Initial Release (Due: 2025-12-15)
v1.1.0 - Export Features (Due: 2026-01-31)
```

---

## Related Documents

- [04_DEVELOPMENT_STANDARDS.md](04_DEVELOPMENT_STANDARDS.md) - Commit linking
- [09_BUILD_DEPLOYMENT.md](09_BUILD_DEPLOYMENT.md) - Release process

---

**Last Updated:** November 2025  
**Version:** 1.0.0

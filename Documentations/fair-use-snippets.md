# Short Snippets Under Fair Use

Some jurisdictions allow limited use of copyrighted works without permission under "fair use" or "fair dealing" doctrines, especially for commentary, criticism, or educational purposes. A quiz that plays very short excerpts (e.g., 5–15 seconds) could fall into this category when shared privately among friends.

For version 1 of the app this is the core approach: creators provide tiny clips of well‑known songs that are short enough to hint at the track without substituting for it. The documentation below should guide future implementations that rely on the same concept.

## Implementation Notes
- Enforce a maximum clip length (e.g., 6–10 seconds) when importing audio and warn if a clip exceeds this limit.
- Include disclaimers that quiz creators are responsible for ensuring their use qualifies as fair use in their jurisdiction and that the operator may remove content on complaint.
- Offer tools to trim, fade, and normalize clips so only a brief, recognizable portion is used.
- When a quiz is shared, strip any metadata that could expose full-song details and optionally delete the quiz automatically after a short time to minimize distribution.

## Advantages
- Users can reference familiar songs without needing licenses.
- Very small clips reduce the likelihood of takedown requests.

## Considerations
- Fair use is a legal defense, not a guaranteed right; the app operator could still receive complaints.
- This approach may be unsuitable for commercial distribution or public quizzes; a different licensing model may be required for later versions.

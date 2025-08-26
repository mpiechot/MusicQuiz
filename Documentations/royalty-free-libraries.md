# Royalty-Free Music Libraries

Stock music providers sell or offer subscriptions that grant broad reuse rights. By sourcing quiz tracks from a library like [Bensound](https://www.bensound.com/), [Incompetech](https://incompetech.com/), or commercial services (Artlist, Epidemic Sound), the app can include high-quality music without complex licensing negotiations.

## Implementation Notes
- Acquire a license or subscription that allows redistribution of short clips within an interactive app.
- Host the licensed files yourself or bundle them with the app; avoid hot-linking to the provider unless the license explicitly allows it.
- Maintain a manifest that records where each clip came from and the license terms.
- Consider offering themed packs (e.g., jazz, electronic) based on the purchased catalog.

## Advantages
- Professional recordings with clear, straightforward licenses.
- Predictable costs and no need for external API integrations.
- The same licensed clips can be reused across multiple quizzes.

## Considerations
- Upfront licensing fees or recurring subscription costs.
- Each provider's terms differ; ensure the license covers redistribution and quiz-style usage.

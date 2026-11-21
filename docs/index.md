---
layout: default
title: Home
---

# Combat Systems Devlog

Welcome to my development blog!

I'm learning combat programming and game design
in Unity, documenting my experiments, challenges,
and improvements along the way.

## Development Logs

{% for post in site.posts %}
### [{{ post.title }}]({{ post.url | relative_url }})
{{ post.excerpt }}
{% endfor %}

---
layout: default
title: Home
---

# Combat Systems Devlog

Learning Combat Systens in Unity! I will be sharing what I learn here.

My goals involve learning design behind combat mechanics and design them myself, then refactor and improve upon it using AI. 


## Development Logs

{% for post in site.posts %}
### [{{ post.title }}]({{ post.url | relative_url }})
{{ post.excerpt }}
{% endfor %}

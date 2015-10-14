# CS431 Project

# Team
- [Adam](https://github.com/adamr5000)
- [George](https://github.com/GeorgeHahn)
- [Sai](https://github.com/somsai002)

# Setup

- Visual Studio
- Postgres - use a password of 'password' when you install (or set up a .config file so we can all have whatever password we want)
- [pgcli](http://pgcli.com/) - helpful for working with Postgres
- [PostgreSQL Command Line Cheat Sheet](https://gist.github.com/jmeridth/f2ad6b580ae18501c538#file-postgresql_command_line_cheat_sheet-md)

```
	git clone
	Open project in visual studio
	Build (may have to press twice to let NuGet download packages)
	Run
```

# Docs
[Nancy docs](https://github.com/NancyFx/Nancy/wiki/Documentation)

# Notes
- View engines used: [Razor](https://github.com/aspnet/Razor), <s>[Markdown](http://blog.jonathanchannon.com/2013/04/08/using-a-markdown-viewengine-with-nancy/)</s> The Markdown view engine doesn't work with Autofac and I don't care to fix it.
- IOC used: [Autofac](http://autofac.org/)
- Code attempts to detect whether your dev machine is running MySQL or Postgres. The expected default password for each is 'password'.
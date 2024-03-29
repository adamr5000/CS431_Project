# CS431 Project

[![Build status](https://ci.appveyor.com/api/projects/status/dvgcj4bd9cqwyywm/branch/master?svg=true)](https://ci.appveyor.com/project/GeorgeHahn/cs431-project/branch/master)

# Team
- [Adam](https://github.com/adamr5000)
- [George](https://github.com/GeorgeHahn)
- [Sai](https://github.com/somsai002)

# Setup

- Visual Studio
- Database can be MySQL or Postgres (Mysql auth should be root:password, Postgres should be postgres:password)

```
	git clone
	Open project in visual studio
	Build
	Run
	See localhost:8080
```

# Docs
[Nancy docs](https://github.com/NancyFx/Nancy/wiki/Documentation)

# Notes
- View engines used: [Razor](https://github.com/aspnet/Razor), <s>[Markdown](http://blog.jonathanchannon.com/2013/04/08/using-a-markdown-viewengine-with-nancy/)</s> The Markdown view engine doesn't work with Autofac and I don't care to fix it.
- IOC used: [Autofac](http://autofac.org/)
- Code attempts to detect whether your dev machine is running MySQL or Postgres. The expected default password for each is 'password'. If this fails on your machine, open an issue and I'll create proper database configuration files. 
- I use [Sentinel Log Viewer](https://sentinel.codeplex.com/), but anything that supports NLog (and family) should work. (Use the default Sentinel settings for the current nlog.config).

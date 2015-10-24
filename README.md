# CS431 Project

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

var gulp = require('gulp');
var less = require('gulp-less');
var path = require('path');
var plumber = require('gulp-plumber');
var concat = require('gulp-concat');
var fs = require("fs");
var spawn = require('child_process').spawn;
var spawnSync = require('child_process').spawnSync;
var path = require("path");

var wwwRoot = path.resolve(".\\wwwroot");
var htmlPath = "./build"
var assetsPath = wwwRoot + "/assets";
var srcPath = "./src/Okarta.Frontend"


gulp.task('default', ['livereload', 'build', 'watch', 'serve'],function() {
});

gulp.task('build', ['html', 'templates', "msbuild", 'vendor', 'js', 'less', 'image'],function() {
});


gulp.task('vendor', function() {
  gulp.src(['bower_components/**/*.js','bower_components/**/*.map', 'bower_components/**/*.css']).
  pipe(plumber()).
  pipe(gulp.dest(assetsPath + '/vendor')).
  pipe(livereload({
        auto: false
  }));
})

gulp.task('js', function() {

  gulp.src([
    srcPath + '/js/**/module.js',
    srcPath + '/js/**/*.js']).
  pipe(plumber()).
  pipe(concat('main.js')).
  pipe(gulp.dest(assetsPath + '/js')).
  pipe(livereload({
        auto: false
  }));

  gulp.src([srcPath + '/js/data/*.json'])
  .pipe(plumber())
  .pipe(gulp.dest(assetsPath + '/js/data'));

});

gulp.task('html', function () {
    return gulp.src(srcPath + '/index.html')
        .pipe(plumber())
        .pipe(gulp.dest('build/'))
        .pipe(livereload({
              auto: false
        }));
});


gulp.task('msbuild', function() {
  if(hostProcess) {
    hostProcess.kill();
  }
  var msbuild = "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\msbuild.exe"
  var argsHost = [".\\src\\Okarta.Web.Host\\Okarta.Web.Host.csproj",  "/nologo", "/verbosity:n",  "/t:Build", "/p:Configuration=Debug"]
  var msbuildHost = spawnSync(msbuild, argsHost);
  process.stdout.write(msbuildHost.stdout);


  var argsWeb = [".\\src\\Okarta.Web\\Okarta.Web.csproj", "/nologo", "/verbosity:m",  "/t:Build",
    "/t:pipelinePreDeployCopyAllFilesToOneFolder",
    "/p:AutoParameterizationWebConfigConnectionStrings=false;Configuration=Debug",
    "/p:SolutionDir=.\\"];

  var msbuildWeb = spawnSync(msbuild, argsWeb);
  if(msbuildWeb.stdout) {
    process.stdout.write(msbuildWeb.stdout);
  }
  if(msbuildWeb.stderr) {
    process.stderr.write(msbuildWeb.stderr);
  }

  return gulp.src('./src/Okarta.Web.Host/bin/Debug/**')
        .pipe(plumber())
        .pipe(gulp.dest(wwwRoot + '/bin'));
});


var templateCache = require('gulp-angular-templatecache');

gulp.task('templates', function() {
  gulp.src(srcPath + '/templates/**/*.html')
        .pipe(plumber())
        .pipe(templateCache({standalone: true}))
        .pipe(gulp.dest(assetsPath + '/js'))
        .pipe(livereload({
              auto: false
        }));
})


gulp.task('image', function() {
  gulp.src([srcPath + '/img/**/*.png', srcPath + '/img/**/*.jpg']).
  pipe(plumber()).
  pipe(gulp.dest(assetsPath + '/img'))
});

gulp.task('less', function () {
  gulp.src(srcPath + '/less/main.less').
  pipe(plumber()).
  pipe(less({
    paths: [ path.join(__dirname, srcPath + '/less') ]
  })).
  pipe(gulp.dest(assetsPath + '/css')).
  pipe(livereload({
        auto: false
  }));
});

gulp.task('watch', function() {
  gulp.watch([
    srcPath + '/less/**/*.less'
	], ['less']);
  gulp.watch([
    srcPath + '/js/**/*.js',
    srcPath + '/js/data/**/*.json'
  ], ['js']);
  gulp.watch([
    srcPath + '/img/**/*.png',
    srcPath + '/img/**/*.jpg'
  ], ['image']);
  gulp.watch([
    'index.html'
  ], ['html'])
  gulp.watch([
    srcPath + '/index.html'
  ], ['html']);
  gulp.watch([
    srcPath + '/templates/**/*.*'
  ], ['templates']);
  gulp.watch(["./bower_components/**/*.*"], ['vendor']);
  gulp.watch(["./src/**/*.cs", "./src/**/*.csproj"], ["msbuild", 'serve'])
});

var exec = require('child_process').exec,
	connect = require('connect'),
	connectLivereload = require('connect-livereload'),
	connectServeStatic = require('serve-static'),
	http = require('http'),
	open = require('open');

var hostProcess;
gulp.task('serve', ['msbuild'], function() {
  var hostPath = path.resolve(".\\wwwroot\\bin\\Okarta.Web.Host.exe");

  hostProcess = spawn(hostPath, [], {detached: true, cwd: ".\\wwwroot"});

  hostProcess.stdout.on('data', function (data) {
    console.log('stdout: ' + data);
  });

  hostProcess.stderr.on('data', function (data) {
    console.log('stderr: ' + data);
  });

  hostProcess.on('close', function (code) {
    console.log('child process exited with code ' + code);
  });

  function onClose() {
    hostProcess.kill();
		exec('gulp clean', function() {
			process.exit(0);
		});
  }

  process.on("SIGINT", onClose);
  process.on("SIGTERM", onClose);
});

var gulp = require('gulp'),
	livereload = require('gulp-livereload');

gulp.task('livereload', function() {
	livereload.listen();
});

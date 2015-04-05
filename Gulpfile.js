var gulp = require('gulp');
var less = require('gulp-less');
var path = require('path');
var plumber = require('gulp-plumber');
var concat = require('gulp-concat');
var fs = require("fs");

var htmlPath = "./build"
var assetsPath = "./src/Okarta.Web/assets";
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
  exec("msbuild", function(error, stdout, stderr) {
    console.log('stdout: ' + stdout);
    console.log('stderr: ' + stderr);
    if (error !== null) {
        console.log('exec error: ' + error);
    }
  });
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
  gulp.watch(["./src/**/*.cs"], ["msbuild"])
});

var exec = require('child_process').exec,
	connect = require('connect'),
	connectLivereload = require('connect-livereload'),
	connectServeStatic = require('serve-static'),
	http = require('http'),
	open = require('open'),
  path = require("path");

gulp.task('serve', function() {
  var applicationPath = path.resolve(".\\src\\Okarta.Web");
  var iisExpressPath = "C:\\Program Files (x86)\\IIS Express\\iisexpress.exe"
  var args = ["/path:" + applicationPath, "/port:9002"]
  var spawn = require('child_process').spawn;
  var iisexpress = spawn(iisExpressPath, args, {detached: true});

  iisexpress.stdout.on('data', function (data) {
    console.log('stdout: ' + data);
  });

  iisexpress.stderr.on('data', function (data) {
    console.log('stderr: ' + data);
  });

  iisexpress.on('close', function (code) {
    console.log('child process exited with code ' + code);
  });

	// Clean on exit
  function onClose() {
    iisexpress.kill();
		exec('gulp clean', function() {
			process.exit(0);
		});
  }
  process.on("uncaughtException", onClose);
  process.on("SIGINT", onClose);
  process.on("SIGTERM", onClose);
});

var gulp = require('gulp'),
	livereload = require('gulp-livereload');

gulp.task('livereload', function() {
	livereload.listen();
});

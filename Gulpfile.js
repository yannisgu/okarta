var gulp = require('gulp');
var less = require('gulp-less');
var path = require('path');
var plumber = require('gulp-plumber');
var concat = require('gulp-concat');
var fs = require("fs");

var htmlPath = "./build"
var assetsPath = "./build/assets";
var srcPath = "./src/Okarta.Frontend"


gulp.task('default', ['livereload', 'build', 'watch', 'serve'],function() {
});

gulp.task('build', ['html', 'templates', 'vendor', 'js', 'less', 'image'],function() {
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


var templateCache = require('gulp-angular-templatecache');

gulp.task('templates', function() {
  gulp.src(srcPath + '/templates/**/*.html')
        .pipe(plumber())
        .pipe(templateCache({standalone: true}))
        .pipe(gulp.dest('build/assets/js'))
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
});

var exec = require('child_process').exec,
	connect = require('connect'),
	connectLivereload = require('connect-livereload'),
	connectServeStatic = require('serve-static'),
	http = require('http'),
	open = require('open');

gulp.task('serve', function() {
	var app = connect()
			.use(connectLivereload())
			.use(connectServeStatic('build'))
            .use(connectServeStatic(assetsPath + '/..')),
		server = http.createServer(app).listen(9002);

	server.on('listening', function() {
		open('http://localhost:9002');
	});

	// Clean on exit
	process.on('SIGINT', function() {
		exec('gulp clean', function() {
			process.exit(0);
		});
	});
});

var gulp = require('gulp'),
	livereload = require('gulp-livereload');

gulp.task('livereload', function() {
	livereload.listen();
});

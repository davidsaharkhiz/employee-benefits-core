var gulp = require('gulp');
var sass = require('gulp-sass');
var browserSync = require('browser-sync').create();
var header = require('gulp-header');
/*
var cleanCSS = require('gulp-clean-css');
var pug = require('gulp-pug');
var rename = require("gulp-rename");
var uglify = require('gulp-uglify');
var beautify = require('gulp-html-beautify');*/
var pkg = require('./package.json');

//todo: we should probably restore the rest, but for now this is all we need from node/gulp for demo purposes

// Set the banner content
var banner = ['/*!\n',
  ' * Start Bootstrap - <%= pkg.title %> v<%= pkg.version %> (<%= pkg.homepage %>)\n',
  ' * Copyright 2013-' + (new Date()).getFullYear(), ' <%= pkg.author %>\n',
  ' * Licensed under <%= pkg.license %> (https://github.com/BlackrockDigital/<%= pkg.name %>/blob/master/LICENSE)\n',
  ' */\n',
  ''
].join('');

// Compiles SCSS files from /scss into /css
gulp.task('sass', function() {
	return gulp.src('wwwroot/scss/sb-admin.scss')
    .pipe(sass())
    .pipe(header(banner, {
      pkg: pkg
    }))
    .pipe(gulp.dest('css'))
    .pipe(browserSync.reload({
      stream: true
    }))
});

// Default task
gulp.task('default', ['sass']);
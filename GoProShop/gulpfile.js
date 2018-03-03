/// <binding BeforeBuild='sass' AfterBuild='sass' />
var sass = require("gulp-sass");
var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var autoprefixer = require('gulp-autoprefixer');

var paths = {
    webroot: "./Content/"
}

paths.scss = paths.webroot + "Sass/**/*.scss"; 

gulp.task('sass', function () {
   return gulp.src(paths.scss)
       .pipe(sass({ outputStyle: 'compressed' }))
       .pipe(concat('styles.css'))
       .pipe(autoprefixer({ browsers:["last 2 versions"] }))
       .pipe(gulp.dest(paths.webroot + "Css/"));
});
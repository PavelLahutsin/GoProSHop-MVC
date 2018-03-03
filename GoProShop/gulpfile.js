/// <binding BeforeBuild='sass' AfterBuild='sass' />
var sass = require("gulp-sass");
var gulp = require('gulp');

var paths = {
    webroot: "./Content/"
}

paths.scss = paths.webroot + "Sass/**/*.scss"; 

gulp.task('sass', function () {
    gulp.src(paths.scss)
        .pipe(sass())
        .pipe(gulp.dest(paths.webroot + "Css/"));
});
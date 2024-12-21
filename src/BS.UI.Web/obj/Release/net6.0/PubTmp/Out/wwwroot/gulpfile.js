const gulp = require('gulp');
const sass = require('gulp-sass')(require('sass'));
const autoprefixer = require('gulp-autoprefixer');
const cleanCSS = require('gulp-clean-css');

function scss() {
    return gulp.src('./assets/scss/**/*.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(autoprefixer())
        .pipe(cleanCSS({ debug: true }, (details) => {
            console.log(`${details.name}: Original size: ${details.stats.originalSize} - Minified size: ${details.stats.minifiedSize}`);
        }))
        .pipe(gulp.dest('./assets/css/'));
}

function watch() {
    gulp.watch('./assets/scss/**/*.scss', scss);
}

exports.scss = scss;
exports.watch = watch;
exports.default = gulp.series(scss, watch);
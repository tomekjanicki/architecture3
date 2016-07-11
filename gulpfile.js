var gulp = require("gulp");
var bower = require("gulp-bower");
var del = require("del");
var minifyCSS = require("gulp-minify-css");
var sourcemaps = require("gulp-sourcemaps");
var concat = require("gulp-concat");
var uglify = require("gulp-uglify");

var root = "projects/Architecture3.Web/Static/";

var config = {
    jquerysrc: [
        "bower_components/jquery/dist/jquery.min.js"
    ],
    jquerybundle: root + "Scripts/jquery.min.js",

    bootstrapsrc: [
        "bower_components/bootstrap/dist/js/bootstrap.min.js"
    ],
    bootstrapbundle: root + "Scripts/bootstrap.min.js",

    bootstrapcss: "bower_components/bootstrap/dist/css/bootstrap.css",
    boostrapfonts: "bower_components/bootstrap/dist/fonts/*.*",

    fontsout: root + "Content/fonts",
    cssout: root + "Content/css"
}

gulp.task("clean-vendor-scripts", function () {
    return del([config.jquerybundle, config.bootstrapbundle]);
});

gulp.task("jquery-bundle", ["clean-vendor-scripts", "bower-restore"], function () {
    return gulp.src(config.jquerysrc)
     .pipe(sourcemaps.init())
     .pipe(concat("jquery.min.js"))
     .pipe(sourcemaps.write("maps"))
     .pipe(gulp.dest(root + "Scripts"));
});

gulp.task("bootstrap-bundle", ["clean-vendor-scripts", "bower-restore"], function () {
    return gulp.src(config.bootstrapsrc)
     .pipe(sourcemaps.init())
     .pipe(concat("bootstrap.min.js"))
     .pipe(sourcemaps.write("maps"))
     .pipe(gulp.dest(root + "Scripts"));
});

gulp.task("vendor-scripts", ["jquery-bundle", "bootstrap-bundle"], function () {
});

gulp.task("clean-styles", function () {
    return del([config.fontsout, config.cssout]);
});

gulp.task("css", ["clean-styles", "bower-restore"], function () {
    return gulp.src([config.bootstrapcss])
     .pipe(sourcemaps.init())
     .pipe(minifyCSS())
     .pipe(concat("app.min.css"))
     .pipe(sourcemaps.write("maps"))
     .pipe(gulp.dest(config.cssout));
});

gulp.task("fonts", ["clean-styles", "bower-restore"], function () {
    return gulp.src(config.boostrapfonts).pipe(gulp.dest(config.fontsout));
});

gulp.task("styles", ["css", "fonts"], function () {
});

gulp.task("bower-restore", function () {
    return bower();
});

gulp.task("default", ["vendor-scripts", "styles"], function () {
});

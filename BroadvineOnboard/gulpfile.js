/// <binding BeforeBuild='default' ProjectOpened='watch' />
// For Bower Components ====

// include plug-ins
var gulp = require('gulp');
var concat = require('gulp-concat');
var minifyCSS = require('gulp-clean-css');
var minifyJS = require('gulp-uglify');
var del = require('del');
var sourcemaps = require('gulp-sourcemaps');
var livereload = require('gulp-livereload');

var config = {
    // JQuery (and related) javascript
    jquerysrc: [
        'bower_components/jquery/dist/jquery.js',
        'bower_components/jquery-validation/dist/jquery.validate.js',
        'bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js'
    ],
    jquerybundle: 'Scripts/jquery-bundle.min.+(js|js.map)',

    // Bootstrap (and respond since it's related) javascript
    bootstrapsrc: [
        'bower_components/bootstrap/dist/js/bootstrap.js',
        'bower_components/respond-minmax/src/respond.js'
    ],
    bootstrapbundle: 'Scripts/bootstrap-bundle.min.+(js|js.map)',

    // Kendo javascript
    kendosrc: [
        'bower_components/kendo-ui/js/kendo.ui.core.min.js'
    ],
    kendobundle: 'Scripts/kendo-bundle.min.+(js|map.js)',

    // Content
    bootstrapcss: 'bower_components/bootstrap/dist/css/bootstrap.css',
    boostrapfonts: 'bower_components/bootstrap/dist/fonts/*.*',
    kendocss: 'bower_components/kendo-ui/styles/kendo.+(common|default).min.css',
    appcss: 'Content/styles/Site.css',

    // Content bundle
    appcssbundle: 'Content/styles/app.min.+(css|css.map)',

    // Out directory for content
    fontsout: 'Content/fonts',
    cssout: 'Content/styles'
}

// Synchronously delete the output script file(s)
gulp.task('clean-existing', function (cb) {
    del([config.jquerybundle,
        config.bootstrapbundle,
        config.kendobundle,
        config.appcssbundle], cb);
});

// Create a jquery bundled file
gulp.task('bundle-jquery', function () {
    return gulp.src(config.jquerysrc)
        .pipe(sourcemaps.init())
        .pipe(minifyJS())
        .pipe(concat('jquery-bundle.min.js'))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest('Scripts'));
});

// Create a bootstrap bundled file
gulp.task('bundle-bootstrap', function () {
    return gulp.src(config.bootstrapsrc)
        .pipe(sourcemaps.init())
        .pipe(minifyJS())
        .pipe(concat('bootstrap-bundle.min.js'))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest('Scripts'));
});

// Create a kendo bundled file
gulp.task('bundle-kendo', function () {
    return gulp.src(config.kendosrc)
        .pipe(sourcemaps.init())
        .pipe(concat('kendo-bundle.min.js'))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest('Scripts'));
})

// Combine and minify all css to 'cssout' directory
gulp.task('minify-combine-css', function () {
    return gulp.src([config.bootstrapcss, config.appcss, config.kendocss])
        .pipe(sourcemaps.init())
        .pipe(minifyCSS())
        .pipe(concat('app.min.css'))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(config.cssout))
        .pipe(livereload());
});

// Send all fonts to 'fontsout' directory
gulp.task('pipe-fonts', function () {
    return gulp.src(config.boostrapfonts)
        .pipe(gulp.dest(config.fontsout));
});

// Combining all scripts task (for organization)
gulp.task('scripts', ['bundle-jquery', 'bundle-bootstrap', 'bundle-kendo'], function () {

});

// Combining all content-related tasks
gulp.task('content', ['minify-combine-css', 'pipe-fonts'], function () {

});

gulp.task('watch', function () {
    livereload.listen();
    gulp.watch(config.appcss, ['minify-combine-css']);
});

gulp.task('default', ['clean-existing', 'scripts', 'content'], function () {

});
module.exports = function (grunt) {
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-concat-sourcemap');
    grunt.loadNpmTasks('grunt-contrib-copy');

    grunt.initConfig({
        concat_sourcemap: {
            options: {
                sourcesContent: true
            },
            target: {
                files: {
                    'wwwroot/app.js': ['Scripts/app.js', 'Scripts/**/*.js']
                }
            }
        },

        copy: {
            css: {
                files: [
                    { expand: true, cwd: "Scripts/", src: ['*.css'], dest: 'wwwroot/' }
                ]
            }
        },

        watch: {
            scripts: {
                files: ['Scripts/**/*.js'],
                tasks: ['concat_sourcemap']
            }
        }
    });

    grunt.registerTask('default', ['concat_sourcemap', 'copy:css', 'watch']);
};
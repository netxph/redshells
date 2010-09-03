desc "default task"
task :default => [:build]

desc "build"
task:build do
  sh "msbuild"
end

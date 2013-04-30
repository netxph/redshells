desc 'default task'
task :default => [:build]

desc 'build'
task :build do
  sh 'msbuild'
end

desc 'rebuild'
task :rebuild do
  sh 'msbuild /t:clean;rebuild'
end

desc 'test'
task :test do
  sh 'xunit.console.clr4.exe bin/debug/RedShells.Tests.dll'
end
  


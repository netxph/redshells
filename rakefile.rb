desc 'default task'
task :default => [:build]

desc 'build'
task :build do
  Rake::Task["uninstall"].invoke
  sh 'msbuild'
  Rake::Task["install"].invoke
end

desc 'rebuild'
task :rebuild do
  sh 'msbuild /t:clean;rebuild'
end

desc 'install'
task :install do
  sh 'installutil bin/debug/RedShells.dll'
end

desc 'uninstall'
task :uninstall do
  sh 'installutil bin/debug/RedShells.dll'
end

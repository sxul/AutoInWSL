#!/bin/bash
echo Link WSL commands to Windows.
if [ ! -d "autocmd" ]; then 
  echo Command folder doesn\'t exist!
  echo Run deploy.bat first
  read -n 1 -p "Press any key to continue..." INP
	if [ '$INP' != '' ] ; then
        echo -ne '\b \n'
	fi
  exit 1;
fi
cd $(dirname $0)
rm -rf autocmd/*.sh
for cmd in `awk "/^[^#;]/{print}" commands`
do
  ln -P -f lazybox autocmd/$cmd.sh
  echo [$cmd] is available. 
done

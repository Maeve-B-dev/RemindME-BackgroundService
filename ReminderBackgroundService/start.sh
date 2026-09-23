#!/bin/bash
echo "Attempting to start reminderService..."
if [ ! -f ./ReminderBackgroundService ]; then
	echo "Program executable not found. Are you in the correct folder?"
fi
mkdir tmp
if [ $? != 0 ]; then
	echo "Program is either already running or did not shut down correctly. Stop program, delete ./tmp and continue?"
	read -n 1 -p "[y/N]:" yn
	case $yn in
		[Yy]* ) nohup ./stop.sh 
		mkdir tmp break;;
		* ) exit 1;;
	esac
fi
nohup ./ReminderBackgroundService > /tmp/current.log 2>&1 &
echo $! > ./tmp/pid.tmp

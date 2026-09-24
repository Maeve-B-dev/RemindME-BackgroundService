#!/bin/bash
echo "Attempting to start reminderService..."

# Check for Programm executable.If it is not found, end the programm.
if [ ! -f ./ReminderBackgroundService ]; then
	echo "Program executable not found. Are you in the correct folder?"
	read -p "Press any key to stop programm..."
	exit 1
fi

# Create /tmp/ dir and check if it was sucessfully created.
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

# Start the program as a background service.
nohup ./ReminderBackgroundService > /tmp/current.log 2>&1 &
echo $! > ./tmp/pid.tmp

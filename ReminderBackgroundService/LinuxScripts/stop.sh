#!/bin/bash
x=$(<./tmp/pid.tmp)
kill $x
rm -r ./tmp/

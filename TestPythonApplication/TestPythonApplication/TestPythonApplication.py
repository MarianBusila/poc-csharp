import random
import os
import sys
import subprocess

def main():
	grocery_list = ['Meat', 'Milk', 'Eggs']
	for x in grocery_list:
		print(x)

	test_file = open("testFile.txt", "wb")
	test_file.write("This is a message")
	test_file.close()

	os.remove("testFile.txt")

	p = subprocess.Popen('dir', shell=True, stdout=subprocess.PIPE, stderr=subprocess.STDOUT)
	for line in p.stdout.readlines():
		print line
	retval = p.wait()


if __name__ == "__main__":
    sys.exit(int(main() or 0))
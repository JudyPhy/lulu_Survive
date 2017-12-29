import sys
reload(sys)
sys.setdefaultencoding("utf-8")

import csv
import os
import xlrd
import codecs

def export():
	del_csv()
	rootdir = os.getcwd()
	list = os.listdir(rootdir)
	for i in list:
		if os.path.splitext(i)[1]==".xlsx":
			print "Export ==>> "+i
			xlsx_to_csv(i)
			
def del_csv():
	rootdir = os.path.dirname(os.getcwd())+'\Client\Assets\StreamingAssets\csv\\'
	print rootdir
	list = os.listdir(rootdir)
	for i in list:
		if os.path.splitext(i)[1]==".csv":
			print "Delete ==>> "+rootdir+i			
			os.remove(rootdir+i)
			
def xlsx_to_csv(file):
	workbook = xlrd.open_workbook(file)
	table = workbook.sheet_by_index(0)
	newFile = "..\Client\Assets\StreamingAssets\csv\\"+file[0:len(file)-4]+"csv"  
	with codecs.open(newFile, 'w', encoding='utf-8') as f:
		write = csv.writer(f)
		for row_num in range(table.nrows):
			row_value = table.row_values(row_num)
			write.writerow(row_value)

if __name__ == '__main__':
	export()
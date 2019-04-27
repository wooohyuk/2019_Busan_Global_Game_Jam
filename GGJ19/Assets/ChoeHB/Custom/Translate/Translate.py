import xlrd
import os
import json

import sys
import subprocess

def copy(s):
    if sys.platform == 'win32' or sys.platform == 'cygwin':
        subprocess.Popen(['clip'], stdin=subprocess.PIPE).communicate(s)
    else:
        raise Exception('Platform not supported')

    
def translate_workbook(workbook):
    dic = {}
    for sheet_name in workbook.sheet_names():
        sheet = workbook.sheet_by_name(sheet_name)
        for x in range(1, sheet.ncols):
            language = sheet.cell(0, x).value
            if not(language in dic):
                dic[language] = {}
            dic[language][sheet_name] = {}
            for y, text_translated in enumerate(sheet.col_values(x)):
                if not y:
                    continue
                
                id_text = sheet.cell(y, 0).value
                if id_text == '':
                    continue
                
                dic[language][sheet_name][id_text] = text_translated
    return dic

workbook = xlrd.open_workbook('Translate.xlsx')
json_dic = translate_workbook(workbook)
with open('Translate.txt', 'w') as file:
    txt = json.dumps(json_dic)
    file.writelines(txt)
    pass

input("Translate Success.. Press Any Key")

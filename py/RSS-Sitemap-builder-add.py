#RSS-Sitemap-builder-add.py
#additional codes to zip

import zipfile

#to zip (RSS-Sitemap-builder.py at 150 lines)
def zip_folder(folder, output_zip):
    with zipfile.ZipFile(output_zip, 'w', zipfile.ZIP_DEFLATED) as zipf:
        for root, dirs, files in os.walk(folder):
            for file in files:
                file_path = os.path.join(root, file)
                arcname = os.path.relpath(file_path, folder)
                zipf.write(file_path, arcname)

#execute (RSS-Sitemap-builder.py at 154 lines)
zip_folder(folder_path, zip_output_path)

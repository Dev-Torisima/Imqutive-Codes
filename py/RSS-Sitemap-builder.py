# RSS-Sitemap-builder (feed.xml, sitemap.xml)
# copyright (c) Imqutive 2025

# pip install beautifulsoup4

import os
import datetime
import xml.etree.ElementTree as ET
from bs4 import BeautifulSoup


# target folder path
folder_path = "C:\\~"
# site base url
base_url = "https://example.com"
# site name
site_name = ""
# site description
site_desc = ""

# 除外するファイルパス
expaths = [
    
]

# Sitemapとfeed両方で許される拡張子
allowd1 = [
    ".txt"
]

# Sitemapだけで許される追加の拡張子
allowd2 = [
    ".png",
    ".jpeg",
    ".mp4",
    ".tiff",
    ".ico"
]



# URL変換ルール（サブディレクトリ対応）
def format_url(rel_path, idff):
    name, ext = os.path.splitext(rel_path)

    if (rel_path in expaths):
        return None

    if ext == ".html" or ext == ".htm" or ext == ".php":
        if rel_path.endswith("index.html") or rel_path.endswith("index.htm") or rel_path.endswith("index.php"):
            if os.path.dirname(name).replace("\\", "/") == "":
                return "/"

            return "/" + os.path.dirname(name).replace("\\", "/") + "/"
        return "/" + name.replace("\\", "/")

    elif allowd1.count(ext) > 0:
        return "/" + rel_path.replace("\\", "/")

    elif (idff == 1) and (allowd2.count(ext) > 0):
        return "/" + rel_path.replace("\\", "/")

    else:
        return None

# HTMLの<title>タグ抽出
def extract_html_title(filepath):
    try:
        with open(filepath, "r", encoding="utf-8") as f:
            soup = BeautifulSoup(f, "html.parser")
            title_tag = soup.find("title")
            return title_tag.text.strip() if title_tag else None
    except Exception as e:
        print(f"タイトル取得失敗: {filepath} → {e}")
        return None

# XML整形（インデント追加）
def indent(elem, level=0):
    i = "\n" + level * "  "
    if len(elem):
        if not elem.text or not elem.text.strip():
            elem.text = i + "  "
        for child in elem:
            indent(child, level + 1)
        if not elem.tail or not elem.tail.strip():
            elem.tail = i
    else:
        if level and (not elem.tail or not elem.tail.strip()):
            elem.tail = i

# sitemap.xml の生成
def generate_sitemap(folder):
    urlset = ET.Element("urlset", xmlns="http://www.sitemaps.org/schemas/sitemap/0.9")

    for root, dirs, files in os.walk(folder):
        for filename in files:
            file_path = os.path.join(root, filename)
            rel_path = os.path.relpath(file_path, folder)
            url_path = format_url(rel_path, 1)

            if url_path:
                url = ET.SubElement(urlset, "url")
                ET.SubElement(url, "loc").text = base_url + url_path

                mod_time = os.path.getmtime(file_path)
                lastmod_date = datetime.datetime.fromtimestamp(mod_time).date().isoformat()
                ET.SubElement(url, "lastmod").text = lastmod_date

    indent(urlset)
    tree = ET.ElementTree(urlset)
    tree.write(os.path.join(folder, "sitemap.xml"), encoding="utf-8", xml_declaration=True)

# feed.xml の生成（RSS 2.0）
def generate_feed(folder):
    rss = ET.Element("rss", version="2.0")
    channel = ET.SubElement(rss, "channel")

    ET.SubElement(channel, "title").text = site_name
    ET.SubElement(channel, "link").text = base_url
    ET.SubElement(channel, "description").text = site_desc

    for root, dirs, files in os.walk(folder):
        for filename in files:
            file_path = os.path.join(root, filename)
            rel_path = os.path.relpath(file_path, folder)
            url_path = format_url(rel_path, 2)

            if url_path:
                item = ET.SubElement(channel, "item")
                ext = os.path.splitext(filename)[1]

                if ext == ".html" or ext == ".htm":
                    title_text = extract_html_title(file_path) or os.path.splitext(filename)[0]
                elif allowd1.count(ext) > 0 or ext == ".php":
                    title_text = filename
                else:
                    continue

                ET.SubElement(item, "title").text = title_text
                ET.SubElement(item, "link").text = base_url + url_path

                mod_time = os.path.getmtime(file_path)
                pub_date = datetime.datetime.fromtimestamp(mod_time).strftime("%a, %d %b %Y %H:%M:%S +0900")
                ET.SubElement(item, "pubDate").text = pub_date

    indent(rss)
    tree = ET.ElementTree(rss)
    tree.write(os.path.join(folder, "feed.xml"), encoding="utf-8", xml_declaration=True)


# 実行
generate_sitemap(folder_path)
generate_feed(folder_path)

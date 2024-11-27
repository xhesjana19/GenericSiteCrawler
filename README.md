# SiteCrawler.Console

This project implements a generic site crawler that provides basic services to traverse a complete site tree. 

### Description
The crawler starts from a given URL and visits each page of the site that is reachable via one or more hops within the same domain. It allows the user to execute a custom action on each page found. The solution includes:

1. A **basic crawler component** to traverse the site as described.
2. A **sample Console App** that uses the crawler component to save all crawled pages as static HTML files to the file system.

---

### Deliverables
- **Crawler Component:** A reusable library for site traversal.
- **Console App:** Demonstrates how to use the crawler to save pages as static files.

---

### Steps Implemented
1. **Console App:**
   - Created a Console App named **"GenericSiteCrawler"**, responsible for running the crawling process.
   
2. **HTML Parsing:**
   - Used the `HtmlAgilityPack` library to parse HTML and extract links from web pages.

3. **Saving Crawled Pages:**
   - All crawled pages are saved in a folder named **"CrawledContentPages"**.

4. **Domain Restriction:**
   - The crawler navigates and processes only pages within the same domain as the starting URL.

5. **Error Handling:**
   - Pages that cannot be reached or encounter errors are skipped without interrupting the crawling process.

6. **Target Framework:**
   - The Console App targets **.NET 8.0**.

---

### How to Test It
1. **Run the Console App:**
   - Open the Console App in Visual Studio or from the terminal.
   
2. **Provide a Starting URL:**
   - When prompted, enter the starting URL (e.g., `https://www.example.com/`).

3. **Output:**
   - The application will crawl the website and save all pages to the **"CrawledContentPages"** folder in the application's root directory.

---

### Example Usage
```plaintext
Enter the starting URL:
https://www.example.com/
Starting crawl at: https://www.example.com/
Crawling: https://www.example.com/
Saved: CrawledContentPages/example_com.html
Crawling: https://www.example.com/about
Saved: CrawledContentPages/example_com_about.html
Crawling completed.

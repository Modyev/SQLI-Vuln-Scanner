# SQLI Vuln Scanner

A simple and effective vulnerability testing tool built in C#. This tool scans URLs for potential vulnerabilities by injecting payloads into query parameters and analyzing server responses.

---

# **Features**  
- Accepts user inputted URLs for testing.  
- Automatically detects and extracts query parameters from the URL.  
- Replaces parameter values with predefined payloads to test for vulnerabilities like:  
  - SQL Injection  
- Analyzes server responses for error messages or payload reflections.  
- Supports flexible payload addition for extended testing.

---

# **Usage**  

1. Launch the application.  
2. Enter the target URL (e.g., `http://example.com/page?param=value`).  
3. The tool will:  
   - Extract all query parameters.  
   - Replace each parameter value with testing payloads.  
   - Send HTTP requests to the server and analyze the responses.  
4. View the results to identify potential vulnerabilities.

---

## **Example Output Images**  
**On Vulnerable URL:**

![SQLI VULN website output](https://github.com/user-attachments/assets/14d3de28-d948-4f4d-a5d9-969128cc24f5)

**On Safe URL:**
![SQLI Safe Website Output](https://github.com/user-attachments/assets/f7c24536-b8d3-4b7b-a8cb-287a0117e90a)

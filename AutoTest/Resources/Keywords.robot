*** Keywords ***
Begin Web Test
    Open Browser                about:blank  ${BROWSER}
    Maximize Browser Window
    Set Selenium Speed          0.3

Go to Web Page
    Load page
    Verify startpage loaded

Load page
    Go To                       ${URL}

Verify start page loaded
    ${link_text} = 		        Get Text  class:navbar-brand
    Should Be Equal		        ${link_text}  Webshop

Clear Shopping Cart
     Go to Web Page
     Click Link                 xpath:/html/body/div/main/table/tbody/tr/td[6]/a[2]
     Click Link                 Link:Shopping Cart
     Click Link                 xpath:/html/body/div/main/span/a[1]

Verify Clear Shopping Cart from Products
    ${link_text} =              Get Text  xpath:/html/body/div/main/span/a[1]
    Should Be Equal             ${link_text}  Clear Cart

End Web Test
    Close Browser
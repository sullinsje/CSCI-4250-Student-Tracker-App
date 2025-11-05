'use strict';

async function testProtectedEndpoint() {
    const fetchUrl = 'http://localhost:5050/api/Student/all';
    const token = localStorage.getItem('authToken');
    
    if (!token) {
        console.error('❌ No auth token found. Please log in first.');
        return;
    }
    
    try {
        const response = await fetch(fetchUrl, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}` 
            }
        });

        if (response.ok) {
            const data = await response.json();
            console.log('✅ Access Granted. Student Data:', data);
        } else {
            // Check the status code and read the response body for more detail
            console.error(`❌ Access Denied. Status: ${response.status}`);
            
            // Read the error body if it exists (e.g., ProblemDetails JSON)
            // Note: Use .text() if you suspect the body isn't JSON
            try {
                 const errorData = await response.json();
                 console.error('Server Error Details:', errorData);
            } catch (e) {
                 console.error('Could not parse server error response body as JSON.');
            }
        }
    } catch (error) {
        // This catches network errors, DNS issues, or CORS blockages.
        console.error('Fatal network or fetch error:', error);
    }
}

window.onload = testProtectedEndpoint;
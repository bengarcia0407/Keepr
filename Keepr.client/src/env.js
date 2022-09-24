export const dev = window.location.origin.includes('localhost')
export const baseURL = dev ? 'https://localhost:5001' : ''
export const useSockets = false
export const domain = 'chris-hildebrandt.us.auth0.com'
export const audience = 'https://codeworks.com'
export const clientId = 'wZATBDOHloJbDnriV9bpEWd7zw76vSqr'
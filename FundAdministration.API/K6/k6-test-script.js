import http from 'k6/http';
import { sleep,check } from 'k6';
import { expect } from "https://jslib.k6.io/k6-testing/0.5.0/index.js";

export const options1 = {
  vus: 10,
  duration: '30s',
};

export const options = {
  stages: [
    { duration: "30s", target: 20 },
    { duration: "1m30s", target: 10 },
    { duration: "20s", target: 0 },
  ],
  thresholds : {
    // Latency SLA: 99% of requests must complete in less than 300ms
    http_req_duration: ['p(99)<20'], 

    // Reliability SLA: The failure rate must be less than 1% (0.01)
    http_req_failed: ['rate<0.01'], 
    
    // Optional: Ensure 100% of checks pass (i.e., status code 201 check)
    checks: ['rate>0.99'],
  }
}

export default function() {
  let res = http.get('https://localhost:44391/api/v1/Fund');
  check(res, {
        'is status 200': (r) => r.status === 200,
    });
  
  sleep(1);
}


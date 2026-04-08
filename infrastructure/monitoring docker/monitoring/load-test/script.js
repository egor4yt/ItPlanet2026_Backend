import http from 'k6/http';
import { check } from 'k6';
import { uuidv4 } from 'https://jslib.k6.io/k6-utils/1.4.0/index.js';
export const options = {
    scenarios: {
        registration: {
            executor: "ramping-arrival-rate",
            startRate: 250,
            timeUnit: "1s",
            preAllocatedVUs: 50,
            maxVUs: 250,
            stages:[
                {target: 250, duration: "1m"},
                {target: 300, duration: "1m"},
                {target: 350, duration: "1m"},
                {target: 400, duration: "1m"},
                {target: 450, duration: "1m"},
                {target: 500, duration: "10m"},
                {target: 50, duration: "1m"},
            ]
        }
    }
};

const CANDIDATES_API_URL = __ENV.CANDIDATES_API_URL;

if (!CANDIDATES_API_URL) {
    throw new Error('Error: Environment variable CANDIDATES_API_URL must be defined!');
}
export default function () {
    const uniqueId = uuidv4();

    var registerResult = http.post(
        `${CANDIDATES_API_URL}/candidates`,
        JSON.stringify({
            firstName: uuidv4(),
            lastName: uuidv4(),
            middleName: uuidv4()
        }),
        { headers: { Authorization: `Test ${uniqueId}`, 'Content-Type': 'application/json' } }
    );

    check(registerResult, { 'status is 201': (r) => r.status === 201 });
}
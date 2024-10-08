using System;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace CircuitBreaker
{
    public class JokeService : IJokeService
    {
        private readonly HttpClient _httpClient;

        //count of failed attempts that have been made
        private static int attemptCount = 0;

        //count of failed attempts allowed, post which the circiut will break
        private static int maxAttemptCount = 3;

        //flag to represent if the circuit is open or close
        private static bool isCircuitOpen = false;

        //field to keep a track of the utc time when the circuit was opened/broken
        private static DateTime circuitOpenStartTime = DateTime.MinValue;

        //the timestamp (in millisecond) for which the circuit should remain open and api call attempts should not be mde
        private static int circuitBreakerTimeSpanMilliseconds = 120000;

        public JokeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetRandomJokes()
        {
            try
            {
                //check if the circuit was earlier open can can be closed now
                CheckIfCircuitBreakerTimeStampIsComplete();

                if (isCircuitOpen == false)
                {
                    #region MakeAPICall
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("https://official-joke-api.appspot.com/random_joke")
                    };
                    var response = await _httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;

                    #endregion
                }
                return "Service is not available. Please try after some time";
            }
            catch (Exception ex)
            {

                //in case of exception, if the max attempt of failure is not yet reached, then increase the counter
                if (isCircuitOpen == false && attemptCount < maxAttemptCount)
                {
                    attemptCount++;
                }
                //if the count of max attempt if reached, then open the circuits and retuen message that the service is not available
                if (attemptCount == maxAttemptCount)
                {
                    if (isCircuitOpen == false)
                    {
                        RecordCircuitBreakerStart();
                    }
                    return "Service is not reachable. Please try after some time";
                }
                return ex.Message;
            }
        }


        //method to start the circuit breaker
        //if sets the isCircuitOpen to true and sets the time when the circuit was broken in utc
        private void RecordCircuitBreakerStart()
        {
            circuitOpenStartTime = DateTime.UtcNow;
            isCircuitOpen = true;
        }

        //method to end the circuit breaker
        private void RecordCircuitBreakerEnd()
        {
            circuitOpenStartTime = DateTime.MinValue;
            isCircuitOpen = false;
            attemptCount = 0;
        }

        //check if currently the circuit is broken or not
        private void CheckIfCircuitBreakerTimeStampIsComplete()
        {
            if (isCircuitOpen == true && circuitOpenStartTime.AddMilliseconds(circuitBreakerTimeSpanMilliseconds) < DateTime.UtcNow)
            {
                RecordCircuitBreakerEnd();
            }
        }



        public async Task<string> GetRandomJokesV2()
        {

            try
            {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("https://official-joke-api.appspot.com/random_joke")
                    };
                    var response = await _httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
            }
           
            catch (BrokenCircuitException ex)
            {

                return $"Request failed due to opened circuit: {ex.Message}";
            }
            catch (HttpRequestException httpEx)
            {
                return $"Request failed. StatusCode={httpEx.StatusCode} Message={httpEx.Message}";
            }
        }

       
    }
}


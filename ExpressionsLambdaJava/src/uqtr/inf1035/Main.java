package uqtr.inf1035;

import java.util.ArrayList;
import java.util.List;

public class Main {

    interface Filter {
        boolean filter(int i);
    }

    static void PrintAll(List<Integer> integers, Filter filter) {
        for(int i : integers) {
            if(filter.filter(i))
                System.out.println(i);
        }
    }

    public static void main(String[] args) {
        ArrayList<Integer> integers = new ArrayList<>();
        integers.add(1);
        integers.add(2);
        integers.add(3);
        integers.add(4);
        integers.add(5);
        integers.add(6);
        Filter f = i -> i % 2 == 0;
        PrintAll(integers, f);
    }
}
